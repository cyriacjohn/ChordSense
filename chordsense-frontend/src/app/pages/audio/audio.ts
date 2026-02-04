import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisService } from '../../services/analysis.service';
import { AudioAnalysisResult } from '../../models/audio-analysis-result';
import WaveSurfer from 'wavesurfer.js';

@Component({
  selector: 'app-audio',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './audio.html',
  styleUrl: './audio.css',
})
export class AudioComponent {
  selectedFile: File | null = null;
  loading = false;
  error: string | null = null;
  result: AudioAnalysisResult | null = null;
  confidence = 0;
  isDragging = false;
  wave: WaveSurfer | null = null;

  mediaRecorder: MediaRecorder | null = null;
  audioChunks: Blob[] = [];
  isRecording = false;

  constructor(private analysisService: AnalysisService) { }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) {
      return;
    }
    this.selectedFile = input.files[0];
    this.error = null;
    this.result = null;
    this.loadWaveform();
  }

  analyzeAudio(): void {
    if (!this.selectedFile) {
      this.error = "Please upload or record audio.";
      return;
    }

    this.loading = true;
    this.error = null;
    this.result = null; 

    this.analysisService.analyzeAudio(this.selectedFile).subscribe(
      {
        next: (res) => {
          if (res.success && res.data) {
            this.result = res.data;
            this.confidence = 90 + (this.result.key.charCodeAt(0) % 10);
          }
          else {
            this.error = res.message ?? 'Analysis failed';
          }
          this.loading = false;
        },
        error: () => {
          this.error = 'Server error';
          this.loading = false;
        }
      }
    );  
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    this.isDragging = false;
  }

  onFileDrop(event: DragEvent) {
    event.preventDefault();
    this.isDragging = false;

    const file = event.dataTransfer?.files[0];
    if (file) {
      this.selectedFile = file;
      this.error = null;
      this.result = null;
      this.loadWaveform();
    }
  }

  loadWaveform(): void {
    if (this.wave) {
      this.wave.destroy();
    }

    this.wave = WaveSurfer.create({
      container: '#waveform',
      waveColor: '#94a3b8',
      progressColor: '#6366f1',
      height: 70,
      barWidth: 2
    });

    if (this.selectedFile) {
      this.wave.loadBlob(this.selectedFile);
    }
  }

  async startRecording(): Promise<void> {
    this.error = null;
    this.result = null;
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });

      this.audioChunks = [];
      this.mediaRecorder = new MediaRecorder(stream);

      this.mediaRecorder.ondataavailable = (event: BlobEvent) => {
        if (event.data.size > 0) {
          this.audioChunks.push(event.data);
        }
      };

      this.mediaRecorder.start();
      this.isRecording = true;
    } catch (err) {
      this.error = 'Microphone access denied';
    }
  }

  stopRecording(): void {
    if (!this.mediaRecorder) return;

    this.mediaRecorder.stop();
    this.isRecording = false;

    this.mediaRecorder.onstop = () => {
      const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });

      this.selectedFile = new File(
        [audioBlob],
        'recording.wav',
        { type: 'audio/wav' }
      );
    };
  }
}
