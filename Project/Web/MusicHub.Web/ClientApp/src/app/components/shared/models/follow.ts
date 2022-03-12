import User from './user';

export default interface Follow {
  id: string,
  followedId: string,
  followed: User,
  following: User
}
