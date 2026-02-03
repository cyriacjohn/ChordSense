import { Routes } from '@angular/router';
import { LyricsComponent } from './pages/lyrics/lyrics';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'lyrics',
    pathMatch: 'full'
  },
  {
    path: 'lyrics',
    component: LyricsComponent
  }
];
