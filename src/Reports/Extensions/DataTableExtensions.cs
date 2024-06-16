using System.Data;

namespace Reports.Extensions;

public static class DataTableExtensions
{
    // Преобразование DataTable в HTML.
    public static string ToHtml(this DataTable dt)
    {
        string html = "<table border=\"1\">";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            html += "<tr>";

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
            }

            html += "</tr>";
        }

        html += "</table>";

        return html;
    }
}