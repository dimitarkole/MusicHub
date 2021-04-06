export enum OrderMethod {
  CreatedOnAsc = 0,
  CreatedOnDesc = 1,
  NameAsc = 2,
  NameDesc = 3,
}

export function CreateOrderMethod(orderMethod: string) {
  if (orderMethod == "CreatedOnDesc") {
    return OrderMethod.CreatedOnDesc;
  }
  else if (orderMethod == "NameAsc") {
    return OrderMethod.NameAsc;
  }
  else if (orderMethod == "NameDesc") {
     return OrderMethod.NameDesc;
  }

  return OrderMethod.CreatedOnAsc;
}
