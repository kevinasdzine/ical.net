﻿using System;
using System.Collections.Generic;
using System.Text;
using DDay.iCal.Components;

namespace DDay.iCal.Validator.RFC2445
{
	public class EventPriorityPropertyValidator :
        IValidator
	{
        #region Public Properties

        public iCalendar iCalendar { get; set; }

        #endregion

        #region Constructors

        public EventPriorityPropertyValidator(iCalendar cal)
        {
            iCalendar = cal;
        }

        #endregion

        #region IValidator Members

        public IValidationResult[] Validate()
        {
            ValidationResult result = new ValidationResult("eventPriorityProperty");
            result.Passed = true;

            List<IValidationError> errors = new List<IValidationError>();

            foreach (Event evt in iCalendar.Events)
            {
                ValidationResult evtResult = ValidationResult.GetCompositeResults(
                    "eventPriorityProperty",
                    new PropertyCountValidator(evt, "VEVENT", "PRIORITY")                    
                );

                if (!evtResult.Passed.Value)
                {
                    result.Passed = false;
                    errors.AddRange(evtResult.Errors);
                }
            }

            result.Errors = errors.ToArray();
            return new IValidationResult[] { result };
        }

        #endregion
	}
}