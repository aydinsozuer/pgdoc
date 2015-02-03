﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MixERP.Net.Utilities.PgDoc.Helpers;
using MixERP.Net.Utilities.PgDoc.Models;

namespace MixERP.Net.Utilities.PgDoc.Parsers
{
    internal static class ViewParser
    {
        internal static string Parse(string content, List<string> matches, ICollection<PgView> views)
        {
            foreach (string match in matches)
            {
                string comment = HtmlHelper.RemoveComment(match);

                if (!comment.StartsWith("Views"))
                {
                    continue;
                }

                comment = comment.Replace("Views", "");

                string items = "";
                items = views.Aggregate(items, (current, view) => current + Parse(comment, view));

                content = content.Replace(match, items);
            }
            return content;
        }

        internal static string Parse(string content, PgView view)
        {
            StringBuilder items = new StringBuilder();
            items.Append(content.Replace("[Name]", view.Name)
                .Replace("[ViewSchema]", view.SchemaName)
                .Replace("[RowNumber]", view.RowNumber.ToString())
                .Replace("[Owner]", view.Owner)
                .Replace("[Tablespace]", view.Tablespace)
                .Replace("[Definition]", view.Definition)
                .Replace("[Description]", view.Description));

            content = content.Replace(content, items.ToString());

            return content;
        }
    }
}