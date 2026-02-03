import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnalysisService {

  private apiBaseUrl = 'http://localhost:5294/api/analysis';

  constructor(private http: HttpClient) { }

  analyzeLyrics(lyrics: string): Observable<any> {
    return this.http.post<any>(
      `${this.apiBaseUrl}/lyrics`,
      { lyrics }
    );
  }
}

