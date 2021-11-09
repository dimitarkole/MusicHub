import { ChartData } from "./chartData";
import { LineChartSeries } from "./lineChartSeries";

export interface LineChartData {
  name: string,
  Series: LineChartSeries[],
}
