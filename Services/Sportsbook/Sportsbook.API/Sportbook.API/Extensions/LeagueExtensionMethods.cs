﻿using BettingApp.Services.Sportbook.API.DTOs;
using BettingApp.Services.Sportbook.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Extensions
{
    public static class LeagueExtensionMethods
    {
        public static LeagueDTO ToLeagueDTO(this League league)
        {
            return new LeagueDTO()
            {
                Id = league.Id,
                Name = AddSpacesToSentence(league.Name),
            };
        }

        private static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
