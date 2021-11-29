import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Follow from '../models/follow';
import { FollowPagination } from '../models/followPagination';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  private route: string = 'Follow';

  constructor(private http: HttpClient) { }

  allOwnFollowing(page: number) {
    return this.http.get<FollowPagination>(`${this.route}/GetOwnFollowing/${page}`);
  }

  allFollowers(page: number) {
    return this.http.get<FollowPagination>(`${this.route}/GetFollowers/${page}`);
  }

  create(follow: Follow) {
    return this.http.post(this.route, follow);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  isFollowing(id: string) {
    return this.http.get<boolean>(`${this.route}/IsFollowing/${id}`);
  }

  getFollowingId(userId: string) {
    return this.http.get<string>(`${this.route}/GetFollowingId/${userId}`);
  }

  getFollowerId(userId: string) {
    return this.http.get<string>(`${this.route}/GetFollowerId/${userId}`);
  }

  getUserFollowing(userId: string, page: number) {
    return this.http.get<FollowPagination>(`${this.route}/GetFollowing/${userId}/${page}`);
  }

  getUserFollowers(userId: string, page: number) {
    return this.http.get<FollowPagination>(`${this.route}/GetFollowers/${userId}/${page}`);
  }
}
