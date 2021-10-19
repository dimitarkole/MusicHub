import { ChartData } from "./chartData";

export interface Chart<TValue> {
  title: string,
  data: ChartData<TValue>,
}
