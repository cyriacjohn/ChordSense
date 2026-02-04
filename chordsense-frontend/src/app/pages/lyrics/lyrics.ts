import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisService } from '../../services/analysis.service';
import { FormsModule } from '@angular/forms';
import { LyricAnalysisResult } from '../../models/lyric-analysis-result';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-lyrics',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './lyrics.html',
  styleUrls: ['./lyrics.css'],
})


export class LyricsComponent {
  // 1. User input
  lyricsText: string = '';

  //2. UI state
  loading = false;
  error: string | null = null;

  //3. Result from backend
  result: LyricAnalysisResult | null = null;

  constructor(private analysisService: AnalysisService) { }

  analyzeLyrics(): void {
    this.resetState();

    if (!this.lyricsText.trim()) {
      this.error = 'Please enter lyrics';
      this.loading = false;
      return;
    }

    this.loading = true;

    this.analysisService.analyzeLyrics(this.lyricsText).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.result = res.data;
        } else {
          this.error = res.message ?? 'Analysis failed';
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Server error';
        this.loading = false;
      }
    });
  }

  private resetState(): void {
    this.error = null;
    this.result = null;
  }

  get sentimentClass(): string {
    if (!this.result) return '';
    return this.result.sentiment;
  }

  getSentimentExplanation(): string {
    if (!this.result) return '';

    switch (this.result.sentiment) {
      case 'positive':
        return 'The lyrics express uplifting or hopeful emotions';
      case 'negative':
        return 'The lyrics convey sadness, struggle or emotional weight';
      case 'neutral':
        return 'The lyrics are emotionally balanced and observational';
      default:
        return '';
    }
  }

  getConfidence(): number {
    if (!this.result) return 0;

    switch (this.result.sentiment) {
      case 'positive':
      case 'negative':
        return 85;
      case 'neutral':
        return 65;
      default:
        return 50;
    }
  }

  getSentimentEmoji(): string {
    if (!this.result) return '';

    switch (this.result.sentiment) {
      case 'positive':
        return '😊';
      case 'negative':
        return '😔';
      case 'neutral':
        return '😌';
      default:
        return '';
    }
  }

}
