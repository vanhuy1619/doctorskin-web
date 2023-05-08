using DoctorSkin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DoctorSkin.config
{
    public class SlugifyConfig
    {
        public string slugify(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder slugBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    if (Char.IsLetterOrDigit(c) || c == ' ')
                    {
                        slugBuilder.Append(c);
                    }
                    else if (c == '-')
                    {
                        slugBuilder.Append('-');
                    }
                }
            }

            string slug = slugBuilder.ToString().Trim().ToLowerInvariant();
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");

            return slug;
        }
    }
}