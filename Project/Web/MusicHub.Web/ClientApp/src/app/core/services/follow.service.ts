import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Follow from '../../components/shared/models/follow';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  private route: string = 'Follow';

  constructor(private http: HttpClient) { }

  allFollowed() {
    return this.http.get<Follow[]>(`${this.route}/GetFollowing`);
  }

  allFollowers() {
    return this.http.get<Follow[]>(`${this.route}/GetFollowers`);
  }

  create(follow: Follow) {
    return this.http.post(this.route, follow);
  }

  delete(id: string) {
    return this.http.delete(`${this.route}/${id}`);
  }

  isFollowed(id: string) {
    return this.http.get<boolean>(`${this.route}/IsFollowed/${id}`);
  }

  getFollowId(userId: string) {
    return this.http.get<Follow>(`${this.route}/GetFollowId/${userId}`);
  }

  getFollowerId(userId: string) {
    return this.http.get<string>(`${this.route}/GetFollowerId/${userId}`);
  }

  GetFollowing(userId: string) {
    return this.http.get<Follow[]>(`${this.route}/GetFollowing/${userId}`);
  }

  GetFollowers(userId: string) {
    return this.http.get<Follow[]>(`${this.route}/GetFollowers/${userId}`);
  }
}
