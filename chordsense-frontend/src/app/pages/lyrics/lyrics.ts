import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisService } from '../../services/analysis.service';
import { FormsModule } from '@angular/forms';

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
  result: any = null;

  constructor(private analysisService: AnalysisService) {}

  analyzeLyrics() {
    this.error = null;
    this.result = null;

    if (!this.lyricsText.trim()) {
      this.error = "Please enter lyrics";
      return;
    }

    this.loading = true;

    this.analysisService.analyzeLyrics(this.lyricsText).subscribe({
      next: (res) => {
        if (!res.success) {
          this.error = res.message;
        } else {
          this.result = res.data;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Server error';
        this.loading = false;
      }
    });
  }

}
