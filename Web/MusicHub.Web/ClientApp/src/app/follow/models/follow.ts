import User from "../../shared/models/user";

export default interface Follow {
  id: string,
  followerId: string,
  follower: User,
  following: User
}
