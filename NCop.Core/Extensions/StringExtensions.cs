﻿using System.Text;

namespace NCop.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly int lowerCaseOffset = 'a' - 'A';

        public static string Fmt(this string value, params object[] args) {
            return string.Format(value, args);
        }

        public static string ToCamelCase(this string value) {
            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            var length = value.Length;
            var newValue = new char[length];
            var firstPart = true;

            for (var i = 0; i < length; ++i) {
                var c0 = value[i];
                var c1 = i < length - 1 ? value[i + 1] : 'A';
                var c0isUpper = c0 >= 'A' && c0 <= 'Z';
                var c1isUpper = c1 >= 'A' && c1 <= 'Z';

                if (firstPart && c0isUpper && (c1isUpper || i == 0)) {
                    c0 = (char)(c0 + lowerCaseOffset);
                }
                else {
                    firstPart = false;
                }

                newValue[i] = c0;
            }

            return new string(newValue);
        }

        public static string ToLowercaseUnderscore(this string value) {
            StringBuilder stringBuilder = null;

            if (string.IsNullOrEmpty(value)) {
                return value;
            }

            stringBuilder = new StringBuilder(value.Length);

            foreach (var t in value) {
                if (char.IsLower(t) || t == '_') {
                    stringBuilder.Append(t);
                }
                else {
                    stringBuilder.Append("_");
                    stringBuilder.Append(char.ToLowerInvariant(t));
                }
            }

            return stringBuilder.ToString();
        }

        public static bool IsNullOrEmpty(this string value) {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value) {
            return !value.IsNullOrEmpty();
        }
    }
}
