import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PremiumRequest, PremiumResponse, Occupation } from '../Models/PremiumRequest';

@Injectable({ providedIn: 'root' })
export class PremiumService {
  private http = inject(HttpClient);
  private api = 'https://localhost:7065/api/premium';

  getOccupations() {
    return this.http.get<Occupation[]>(`${this.api}/occupations`);
  }

  calculatePremium(request: PremiumRequest) {
    return this.http.post<PremiumResponse>(`${this.api}/calculate`, request);
  }
}
