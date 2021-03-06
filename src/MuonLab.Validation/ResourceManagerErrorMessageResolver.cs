﻿using System.Globalization;
using System.Resources;

namespace MuonLab.Validation
{
	public sealed class ResourceManagerErrorMessageResolver : IErrorMessageResolver
	{
		readonly ResourceManager resourceManager;
		readonly CultureInfo defaultCulture;

		public ResourceManagerErrorMessageResolver(string assembly)
		{
			this.resourceManager = new ResourceManager(assembly, this.GetType().Assembly);
			this.defaultCulture = CultureInfo.CreateSpecificCulture("en");
		}

		public string GetErrorMessage(ErrorDescriptor error, CultureInfo culture)
		{
			var message = this.resourceManager.GetString(error.Key, culture);
			if (string.IsNullOrEmpty(message))
				message = this.resourceManager.GetString(error.Key, this.defaultCulture);

			if (string.IsNullOrEmpty(message))
				return error.Key;

			foreach (var replacement in error.Replacements)
				message = message.Replace("{" + replacement.Key + "}", replacement.Value.ToString());

			return message;
		}
	}
}