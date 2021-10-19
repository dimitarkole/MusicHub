import { ChartData } from "./chartData";
import { LineChartData } from "./lineChartData";

export interface LineChart{
  name: string,
  xAxisLabel: string,
  yAxisLabel: string,
  data: LineChartData[],
}
