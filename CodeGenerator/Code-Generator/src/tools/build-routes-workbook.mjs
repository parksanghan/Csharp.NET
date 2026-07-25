import fs from "node:fs/promises";
import path from "node:path";
import { fileURLToPath } from "node:url";
import { SpreadsheetFile, Workbook } from "@oai/artifact-tool";

const thisDir = path.dirname(fileURLToPath(import.meta.url));
const repoRoot = path.resolve(thisDir, "..");
const outputDir = path.join(repoRoot, "outputs", "route-generator-example");

const rows = [
  ["Project1.App", "Project2.App", "TCP", "127.0.0.1", 5000, "Project1 sends commands to Project2"],
  ["Project1.App", "DeviceA", "UDP", "239.0.0.10", null, "Project1 sends UDP telemetry broadcast"],
  ["Project2.App", "DeviceB", "TCP", "10.0.0.42", 7001, "Project2 connects to DeviceB"],
  ["Project2.App", "Project1.App", "UDP", "127.0.0.1", null, "Project2 sends heartbeat to Project1"],
];

const workbook = Workbook.create();

const routes = workbook.worksheets.add("Routes");
routes.showGridLines = false;
routes.freezePanes.freezeRows(1);
routes.getRange("A1:F1").values = [["From", "To", "Protocol", "Host", "Port", "Description"]];
routes.getRange("A2:F5").values = rows;

routes.getRange("A1:F1").format = {
  fill: "#1F4E79",
  font: { bold: true, color: "#FFFFFF" },
};
routes.getRange("A1:F5").format.borders = {
  preset: "all",
  style: "thin",
  color: "#D9E2F3",
};
routes.getRange("A:A").format.columnWidth = 18;
routes.getRange("B:B").format.columnWidth = 18;
routes.getRange("C:C").format.columnWidth = 12;
routes.getRange("D:D").format.columnWidth = 16;
routes.getRange("E:E").format.columnWidth = 10;
routes.getRange("F:F").format.columnWidth = 44;
routes.getRange("A2:D5").format = { horizontalAlignment: "left" };
routes.getRange("E2:E5").format = {
  horizontalAlignment: "right",
  numberFormat: "0",
};
routes.getRange("F2:F5").format = { wrapText: true };
routes.getRange("A1:F5").format.autofitRows();
routes.getRange("C2:C100").dataValidation = {
  rule: { type: "list", values: ["TCP", "UDP"] },
};

const table = routes.tables.add("A1:F5", true, "CommunicationRoutesTable");
table.style = "TableStyleMedium2";
table.showFilterButton = true;

const notes = workbook.worksheets.add("Generator Notes");
notes.showGridLines = false;
notes.getRange("A1:D1").merge();
notes.getRange("A1").values = [["Communication Route Generator Sample"]];
notes.getRange("A1").format = {
  fill: "#E2F0D9",
  font: { bold: true, size: 14, color: "#375623" },
};
notes.getRange("A3:D7").values = [
  ["Step", "What happens", null, null],
  ["1", "Edit the Routes sheet as the human source table.", null, null],
  ["2", "Export or copy the same rows into config/communication-routes.csv.", null, null],
  ["3", "The C# source generator reads the CSV as an AdditionalFiles item.", null, null],
  ["4", "Each project gets generated routes where From equals MSBuildProjectName.", null, null],
];
notes.getRange("A3:B7").format.borders = {
  preset: "all",
  style: "thin",
  color: "#D9EAD3",
};
notes.getRange("A3:B3").format = {
  fill: "#548235",
  font: { bold: true, color: "#FFFFFF" },
};
notes.getRange("A:A").format.columnWidth = 10;
notes.getRange("B:B").format.columnWidth = 72;
notes.getRange("B4:B7").format = { wrapText: true };
notes.getRange("A1:B7").format.autofitRows();

await fs.mkdir(outputDir, { recursive: true });

const inspect = await workbook.inspect({
  kind: "table",
  range: "Routes!A1:F5",
  include: "values,formulas",
  tableMaxRows: 10,
  tableMaxCols: 8,
});
console.log(inspect.ndjson);

const errors = await workbook.inspect({
  kind: "match",
  searchTerm: "#REF!|#DIV/0!|#VALUE!|#NAME\\?|#N/A",
  options: { useRegex: true, maxResults: 20 },
  summary: "formula error scan",
});
console.log(errors.ndjson);

const preview = await workbook.render({
  sheetName: "Routes",
  autoCrop: "all",
  scale: 1,
  format: "png",
});
await fs.writeFile(path.join(outputDir, "communication-routes-preview.png"), new Uint8Array(await preview.arrayBuffer()));

const notesPreview = await workbook.render({
  sheetName: "Generator Notes",
  autoCrop: "all",
  scale: 1,
  format: "png",
});
await fs.writeFile(path.join(outputDir, "communication-routes-notes-preview.png"), new Uint8Array(await notesPreview.arrayBuffer()));

const xlsx = await SpreadsheetFile.exportXlsx(workbook);
await xlsx.save(path.join(outputDir, "communication-routes.xlsx"));
