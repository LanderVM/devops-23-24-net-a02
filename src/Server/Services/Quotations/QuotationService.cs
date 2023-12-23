﻿using devops_23_24_net_a02.Shared.Emails;
using Domain.Common;
using Domain.Customers;
using Domain.Exceptions;
using Domain.Formulas;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shared.Common;
using Shared.Common;
using Shared.Customer;
using shared.Equipment;
using shared.Formulas;
using shared.Quotations;
using Shared.Quotations;

namespace Api.Data.Services.Quotations;

public class QuotationService : IQuotationService
{
  private readonly BlancheDbContext _dbContext;

  public QuotationService(BlancheDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<QuotationResult.Dates> GetDatesAsync()
  {
    var query = _dbContext.Quotations
      .Where(q => q.Status == QuotationStatus.Accepted)
      .Select(q =>
        new QuotationDto.Dates { StartTime = q.StartTime.ToUniversalTime(), EndTime = q.EndTime.ToUniversalTime() });

    var dates = await query.ToListAsync();

    var result = new QuotationResult.Dates { DateRanges = dates };
    return result;
  }

  public async Task<QuotationResult.Index> GetIndexAsync()
  {
    var query = _dbContext.Quotations
      .Include(q => q.OrderedBy)
      .ThenInclude(c => c.Email)
      .OrderBy(q => q.Id)
      .Select(q => new QuotationDto.Index
      {
        QuotationId = q.Id,
        Customer = new CustomerDto.Index
        {
          FirstName = q.OrderedBy.FirstName, LastName = q.OrderedBy.LastName, Email = q.OrderedBy.Email.Value
        },
        CreatedAt = q.CreatedAt.ToShortDateString()
      });

    var items = await query.ToListAsync();

    var result = new QuotationResult.Index { Quotation = items };
    return result;
  }

  public async Task<QuotationResult.DetailEdit> GetSpecificDetailEditAsync(int quotationId)
  {
    var quotation = await _dbContext.Quotations.Include(quotation => quotation.QuotationLines)
      .ThenInclude(quotationLine => quotationLine.EquipmentOrdered)
      .ThenInclude(equipment => equipment.Description)
      .Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.Email)
      .Include(quotation => quotation.EventLocation)
      .Include(quotation => quotation.Formula)
      .ThenInclude(formula => formula.Description).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.PhoneNumber).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.VatNumber).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.BillingAddress)
      .FirstOrDefaultAsync(x => x.Id == quotationId);

    if (quotation is null)
    {
      throw new EntityNotFoundException(nameof(Quotation), quotationId);
    }

    var result = new QuotationDto.DetailEdit
    {
      QuotationId = quotation.Id,
      Formula = new FormulaDto.Select { Id = quotation.Formula.Id, Title = quotation.Formula.Description.Title },
      Customer =
        new CustomerDto.Details
        {
          FirstName = quotation.OrderedBy.FirstName,
          LastName = quotation.OrderedBy.LastName,
          Email = new EmailDto.Create { Email = quotation.OrderedBy.Email.Value },
          PhoneNumber = quotation.OrderedBy.PhoneNumber.Value,
          VatNumber = quotation.OrderedBy.VatNumber?.Value,
          BillingAddress =
            new AddressDto
            {
              Street = quotation.OrderedBy.BillingAddress.Street,
              HouseNumber = quotation.OrderedBy.BillingAddress.HouseNumber,
              PostalCode = quotation.OrderedBy.BillingAddress.PostalCode,
              City = quotation.OrderedBy.BillingAddress.City
            }
        },
      EventLocation =
        new AddressDto
        {
          Street = quotation.EventLocation.Street,
          HouseNumber = quotation.EventLocation.HouseNumber,
          City = quotation.EventLocation.City,
          PostalCode = quotation.EventLocation.PostalCode
        },
      Equipment = quotation.QuotationLines.Select(line => new EquipmentDto.LinesDetail
      {
        Amount = line.AmountOrdered,
        EquipmentId = line.EquipmentOrdered.Id,
        Price = line.EquipmentOrdered.Price,
        Name = line.EquipmentOrdered.Description.Title
      }),
      IsTripelBier = quotation.IsTripelBier,
      NumberOfPeople = quotation.NumberOfPeople,
      Opmerking = quotation.Opmerking,
      Status = quotation.Status
    };

    return new QuotationResult.DetailEdit { Quotation = result };
  }

  public async Task<QuotationResponse.Create> CreateAsync(QuotationDto.Create model)
  {
    var chosenFormula = _dbContext.Formulas.FirstOrDefault(formula => formula.Id == model.FormulaId);
    if (chosenFormula is null)
    {
      throw new EntityNotFoundException(nameof(Formula), model.FormulaId);
    }

    if (chosenFormula.IsActive is false)
    {
      throw new ApplicationException($"Formula with id {chosenFormula.Id} is not active!");
    }

    var customer = _dbContext.Customers
      .Include(customer => customer.Email)
      .AsEnumerable()
      .FirstOrDefault(customerFromDb => EqualsCustomer(model, customerFromDb));
    if (customer is null)
    {
      customer = new Customer(
        model.Customer.FirstName,
        model.Customer.LastName,
        GetCustomerEmail(model)
        ?? new Email(model.Customer.Email.Email),
        new BillingAddress(model.Customer.BillingAddress.Street,
          model.Customer.BillingAddress.HouseNumber,
          model.Customer.BillingAddress.City,
          model.Customer.BillingAddress.PostalCode),
        new PhoneNumber(model.Customer.PhoneNumber),
        model.Customer.VatNumber.IsNullOrEmpty() ? null : new VatNumber(model.Customer.VatNumber!));
      _dbContext.Customers.Add(customer);
    }


    var quotationLines = new List<QuotationLine>();
    foreach (var lines in model.Equipments)
    {
      var equipment = _dbContext.Equipments.FirstOrDefault(equipment => equipment.Id == lines.EquipmentId);
      if (equipment is not null)
      {
        quotationLines.Add(new QuotationLine(equipment, lines.Amount));
      }
    }

    if (customer.Email.IsActive is false)
    {
      customer.Email.IsActive = true;
    }

    var quotation = new Quotation(
      chosenFormula,
      customer,
      new EventLocation(
        model.EventLocation.Street,
        model.EventLocation.HouseNumber,
        model.EventLocation.City,
        model.EventLocation.PostalCode
      ),
      quotationLines,
      model.StartTime,
      model.EndTime,
      model.NumberOfPeople,
      model.IsTripelBier
    );

    _dbContext.Quotations.Add(quotation);
    await _dbContext.SaveChangesAsync();
    var result = new QuotationResponse.Create
    {
      QuotationId = quotation.Id,
      FormulaId = chosenFormula.Id,
      EventLocation = new AddressDto
      {
        Street = quotation.EventLocation.Street,
        HouseNumber = quotation.EventLocation.HouseNumber,
        City = quotation.EventLocation.City,
        PostalCode = quotation.EventLocation.PostalCode
      },
      StartTime = quotation.StartTime,
      EndTime = quotation.EndTime,
      Equipments = quotation.QuotationLines
        .Select(line => new EquipmentDto.Lines { EquipmentId = line.Id, Amount = line.AmountOrdered }).ToList(),
      Customer = new CustomerDto.Details
      {
        Id = quotation.OrderedBy.Id,
        FirstName = quotation.OrderedBy.FirstName,
        LastName = quotation.OrderedBy.LastName,
        Email = new EmailDto.Create { Email = quotation.OrderedBy.Email.Value },
        PhoneNumber = quotation.OrderedBy.PhoneNumber.Value,
        VatNumber = quotation.OrderedBy.VatNumber?.Value,
        BillingAddress = new AddressDto
        {
          Street = quotation.OrderedBy.BillingAddress.Street,
          HouseNumber = quotation.OrderedBy.BillingAddress.HouseNumber,
          City = quotation.OrderedBy.BillingAddress.City,
          PostalCode = quotation.OrderedBy.BillingAddress.PostalCode
        }
      },
      IsTripelBier = quotation.IsTripelBier,
      NumberOfPeople = quotation.NumberOfPeople
    };
    return result;
  }

  public async Task<QuotationResult.Detail> GetPriceEstimationDetailsAsync()
  {
    var queryFormulas = _dbContext.Formulas.AsQueryable();

    IEnumerable<FormulaDto.Select> itemsFormulas = await queryFormulas.OrderBy(x => x.Id).Select(
      x => new FormulaDto.Select { Id = x.Id, Title = x.Description.Title }
    ).ToListAsync();


    var queryEquipment = _dbContext.Equipments.AsQueryable();

    IEnumerable<EquipmentDto.Select> itemsEquipment = await queryEquipment.OrderBy(x => x.Id).Select(
      x => new EquipmentDto.Select { Id = x.Id, Title = x.Description.Title }
    ).ToListAsync();


    var queryDates = _dbContext.Quotations.AsQueryable();

    IEnumerable<DateDto> unavailableDates = await queryDates.OrderBy(x => x.StartTime).Select(
      x => new DateDto { StartTime = x.StartTime.ToUniversalTime(), EndTime = x.EndTime.ToUniversalTime() }
    ).ToListAsync();

    var result = new QuotationResult.Detail
    {
      Formulas = itemsFormulas, Equipment = itemsEquipment, UnavailableDates = unavailableDates
    };

    return result;
  }

  public async Task<QuotationResult.Calculation> GetPriceEstimationPrice(QuotationDto.Estimate model)
  {
    var chosenFormula = _dbContext.Formulas.FirstOrDefault(formula => formula.Id == model.FormulaId);
    if (chosenFormula is null)
    {
      throw new EntityNotFoundException(nameof(Formula), model.FormulaId);
    }

    if (chosenFormula.IsActive is false)
    {
      throw new ApplicationException($"Formula with id {chosenFormula.Id} is not active!");
    }

    var queryEquipment = _dbContext.Equipments.AsQueryable();

    var equipmentDtoQuery = new List<EquipmentDto.Index>();

    if (model.EquipmentIds.Any())
    {
      equipmentDtoQuery = await queryEquipment
        .Where(x => model.EquipmentIds.Contains(x.Id))
        .Select(z => new EquipmentDto.Index { Id = z.Id, Price = z.Price }).ToListAsync();
    }

    List<Equipment> equipmentList = new();
    foreach (var item in equipmentDtoQuery)
    {
      Equipment equipment = new(item.Price);
      equipmentList.Add(equipment);
    }

    chosenFormula.Equipment.AddRange(equipmentList);

    var quotation = new Quotation(chosenFormula, new DateTime(model.StartTime), new DateTime(model.EndTime),
      model.EstimatedNumberOfPeople, model.IsTripelBier);
    return new QuotationResult.Calculation { EstimatedPrice = quotation.GetEstimatedPriceRounded() };
  }

  public async Task<QuotationResponse.Create> UpdateAsync(int QuotationId, QuotationDto.Edit model)
  {
    var quotation = _dbContext.Quotations
      .Include(quotation => quotation.QuotationLines)
      .Include(quotation => quotation.OrderedBy).ThenInclude(customer => customer.Email)
      .Include(quotation => quotation.Formula).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.VatNumber).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.PhoneNumber).Include(quotation => quotation.OrderedBy)
      .ThenInclude(customer => customer.BillingAddress).Include(quotation => quotation.EventLocation)
      .FirstOrDefault(quotation => quotation.Id == QuotationId);
    if (quotation is null)
    {
      throw new Exception($"No quotation found with Id: {QuotationId}");
    }

    var quotationLines = new List<QuotationLine>();
    quotationLines.AddRange(from lines in model.EquipmentList
      let equipment = _dbContext.Equipments.FirstOrDefault(equipment => equipment.Id == lines.EquipmentId)
      where equipment is not null
      select new QuotationLine(equipment, lines.Amount)
    );

    quotation.QuotationLines = quotationLines;
    if (model.IsAccepted)
    {
      quotation.Status = QuotationStatus.Accepted;
    }

    quotation.IsTripelBier = model.IsTripelBier;
    quotation.Opmerking = model.Opmerking;

    await _dbContext.SaveChangesAsync();

    var result = new QuotationResponse.Create
    {
      QuotationId = QuotationId,
      FormulaId = quotation.Formula.Id,
      EndTime = quotation.EndTime,
      StartTime = quotation.StartTime,
      Equipments = model.EquipmentList
        .Select(line => new EquipmentDto.Lines { EquipmentId = line.EquipmentId, Amount = line.Amount }).ToList(),
      EventLocation =
        new AddressDto
        {
          City = quotation.EventLocation.City,
          HouseNumber = quotation.EventLocation.HouseNumber,
          PostalCode = quotation.EventLocation.PostalCode,
          Street = quotation.EventLocation.Street
        },
      Customer = new CustomerDto.Details
      {
        Id = quotation.OrderedBy.Id,
        FirstName = quotation.OrderedBy.FirstName,
        LastName = quotation.OrderedBy.LastName,
        Email = new EmailDto.Create { Email = quotation.OrderedBy.Email.Value },
        PhoneNumber = quotation.OrderedBy.PhoneNumber.Value,
        VatNumber = quotation.OrderedBy.VatNumber?.Value,
        BillingAddress = new AddressDto
        {
          Street = quotation.OrderedBy.BillingAddress.Street,
          HouseNumber = quotation.OrderedBy.BillingAddress.HouseNumber,
          City = quotation.OrderedBy.BillingAddress.City,
          PostalCode = quotation.OrderedBy.BillingAddress.PostalCode
        }
      },
      IsTripelBier = quotation.IsTripelBier,
      NumberOfPeople = quotation.NumberOfPeople,
      Opmerking = quotation.Opmerking,
      Status = quotation.Status
    };

    return result;
  }


  private Email? GetCustomerEmail(QuotationDto.Create model)
  {
    return _dbContext.Emails.FirstOrDefault(email => email.Value == model.Customer.Email.Email);
  }

  private static bool EqualsCustomer(QuotationDto.Create model, Customer customerFromDb)
  {
    return customerFromDb.FirstName == model.Customer.FirstName
           && customerFromDb.LastName == model.Customer.LastName
           && customerFromDb.Email.Value == model.Customer.Email.Email
           && customerFromDb.PhoneNumber.Value == model.Customer.PhoneNumber
           && customerFromDb.VatNumber?.Value == model.Customer.VatNumber
           && customerFromDb.BillingAddress.Street == model.Customer.BillingAddress.Street
           && customerFromDb.BillingAddress.HouseNumber == model.Customer.BillingAddress.HouseNumber
           && customerFromDb.BillingAddress.PostalCode == model.Customer.BillingAddress.PostalCode
           && customerFromDb.BillingAddress.City == model.Customer.BillingAddress.City;
  }
}
