import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { NouisliderModule } from 'ng2-nouislider';

import { AppComponent } from './app.component';
import { PhotosService } from './services/photos.service';
import { PhotoComponent } from './ui/photo/photo.component';
import { InfoComponent } from './ui/info/info.component';
import { AutoPlayComponent } from './ui/auto-play/auto-play.component';

@NgModule({
  declarations: [
    AppComponent,
    PhotoComponent,
    InfoComponent,
    AutoPlayComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NouisliderModule,
  ],
  providers: [PhotosService],
  bootstrap: [AppComponent]
})
export class AppModule { }
