using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculator.Application.Common;

public class NoEventExistsException(Guid EventId) : ValidationException($"Event with Id: {EventId} doesn't exist!") { }

public class NotAllowedCalcException(string EventName) : ValidationException($"Calculation for event: {EventName} is not allowed!") { }

public class EndDateGreaterThenStartDateException() : ValidationException("End date greater then start date is ot allowed!") { }

public class JoinDateElapsedException(string EventName) : ValidationException($"Join date for event: {EventName} has elapsed!") { }

public class NoUserAuthenticatedException(string Request) : ValidationException($"No user authenticated for request: {Request}") { }