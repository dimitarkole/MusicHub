import { BarChartSeries } from "./barChartSeries";

export interface BarChart{
  name: string,
  xAxisLabel: string,
  yAxisLabel: string,
  series: BarChartSeries[],
}
