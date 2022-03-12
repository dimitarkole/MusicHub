import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import License from '../../components/shared/models/license';
import MusicLicense from '../../components/shared/models/musicLicense';

@Injectable({
  providedIn: 'root'
})
export class MusicLicenseServiceService {
  private root: string = "LicenseMusic";

  constructor(private http: HttpClient) { }

  all(id: string) {
    return this.http.get<MusicLicense>(`${this.root}/${id}`);
  }

  create(musicLicense: MusicLicense) {
    return this.http.post<string>(`${this.root}/Post`, musicLicense);
  }

  delete(id: string) {
    return this.http.delete(`${this.root}/${id}`);
  }

  deleteAllMusicLicenses(musicId: string) {
    return this.http.delete(`${this.root}/DeleteAllMusicLicenses/${musicId}`);
  }
}
