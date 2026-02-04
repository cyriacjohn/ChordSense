import { Routes } from '@angular/router';
import { LyricsComponent } from './pages/lyrics/lyrics';
import { AudioComponent } from './pages/audio/audio';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'lyrics',
    pathMatch: 'full'
  },
  {
    path: 'lyrics',
    component: LyricsComponent
  },
  {
    path: 'audio',
    component: AudioComponent
  }
];
