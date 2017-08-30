import { Component, OnInit, OnDestroy, NgZone, HostListener, Renderer, ViewChild, Output, EventEmitter } from '@angular/core';
import { PhotosService } from '../../services/photos.service';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit, OnDestroy {
  @ViewChild("backgroundThing") backgroundThing;

  baseUrl: string;
  images: Array<string>;
  currentImageRelative: string;
  currentImageAbsolute: string;
  maxPercent: number;
  minPercent: number;
  sliderPos: number;
  locationString: string;
  dateString: string;
  timeString: string;
  autoplayPaused: boolean = false;

  constructor(private photosService: PhotosService,
              private renderer: Renderer,
              ngZone:NgZone) 
  {
    this.images = photosService.getImages();
    this.baseUrl = photosService.getBaseUrl();
    this.maxPercent = 100;
    this.minPercent = this.maxPercent / this.images.length;
    this.sliderPos = 1;

    setInterval(() => this.autoplayStep(), 500);
  }

  ngOnInit() {
    this.showImageIx(0);
  }

  ngOnDestroy() {
  }

  onSliderUpdate($event)
  {
    let updatedSliderValue: number = $event as number;

    if (updatedSliderValue == this.sliderPos) return;

    if ((Math.abs(updatedSliderValue - this.sliderPos) > 2) && !(updatedSliderValue == 1 && this.sliderPos == this.images.length-1))
    {
      this.autoplayPaused = true;
    }

    let imageIx: number = updatedSliderValue - 1;
    this.showImageIx(imageIx);    
  }


  autoplayStep()
  {
    if (this.autoplayPaused) return;

    if (this.sliderPos >= this.images.length)
    {
      this.sliderPos = 0;
    }
    else
    {
      this.sliderPos++;
    }
    this.showImageIx(this.sliderPos - 1);
  }

  showImageIx(whichIndex: number)
  {
    //console.log(`showImageIx(${whichIndex}`);
    if (whichIndex + 1 != this.sliderPos)
    {
      this.sliderPos = whichIndex + 1;
    }
    this.currentImageRelative = `./assets/${this.images[whichIndex]}`;
    this.currentImageAbsolute = `${this.baseUrl}assets/${this.images[whichIndex]}`;
    this.backgroundThing.nativeElement.style.backgroundImage = `url('${this.currentImageAbsolute}')`;

    this.locationString = "Penstock Lagoon";
    this.dateString = "05 Aug 2017";
    this.timeString = this.currentImageRelative.replace('src/assets/image', '').replace('.jpg', '').split('_')[1].replace('-', ':').substring(0, 5);
  }
  
  onAutoplayChanged(data: boolean)
  {
    this.autoplayPaused = data;
  }

}
