export default interface User {
  id: string,
  username: string,
  userName: string,
  password: string,
  confirmPassword: string,
  currentPassword: string,
  newPassword: string,
  age: number,
  firstName: string,
  lastName: string,
  birthday: Date,
  email: string,
  avater: string,
  imageUrl: string,
  phone: string,
  musicsCount: number,
  playlistsCount: number,
}

export default interface LoginResponse {
  userId: string,
  username: string,
  initials: string,
  roles: string[],
  token: string
}
