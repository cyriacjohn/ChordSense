import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response';
import { LyricAnalysisResult } from '../models/lyric-analysis-result';
import { AudioAnalysisResult } from '../models/audio-analysis-result';

@Injectable({
  providedIn: 'root'
})
export class AnalysisService {

  private apiBaseUrl = 'http://localhost:5294/api/analysis';

  constructor(private http: HttpClient) { }

  analyzeLyrics(lyrics: string): Observable<ApiResponse<LyricAnalysisResult>> {
    return this.http.post<ApiResponse<LyricAnalysisResult>>(
      `${this.apiBaseUrl}/lyrics`,
      { lyrics }
    );
  }

  analyzeAudio(file: File): Observable<ApiResponse<AudioAnalysisResult>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<ApiResponse<AudioAnalysisResult>>(
      `${this.apiBaseUrl}/audio`,
      formData
    );
  }
}

