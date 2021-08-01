export enum OrderMethod {
  CreatedOnAsc = 0,
  CreatedOnDesc = 1,
  NameAsc = 2,
  NameDesc = 3,
  UserNameAsc = 4,
  UserNameDesc = 5,
  EmailAsc = 6,
  EmailDesc = 7.
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
  else if (orderMethod == "UserNameDesc") {
    return OrderMethod.UserNameDesc;
  }
  else if (orderMethod == "UserNameAsc") {
    return OrderMethod.UserNameAsc;
  }
  else if (orderMethod == "EmailDesc") {
    return OrderMethod.EmailDesc;
  }
  else if (orderMethod == "EmailAsc") {
    return OrderMethod.EmailAsc;
  }

  return OrderMethod.CreatedOnAsc;
}
