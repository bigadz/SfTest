import { Component, OnInit, OnDestroy, NgZone, HostListener, Renderer, ViewChild } from '@angular/core';
import { PhotosService } from '../../services/photos.service';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit, OnDestroy {
  @ViewChild("backgroundThing") backgroundThing;
  baseUrl: string;
  windowWidth: number;
  windowHeight: number;
  images: Array<string>;
  currentImageRelative: string;
  currentImageAbsolute: string;
  maxPercent: number;
  minPercent: number;
  locationString: string;
  dateString: string;
  timeString: string;

  touchmoveListenFunc: Function;
  touchendListenFunc: Function;
  touchcancelListenFunc: Function;

  constructor(private photosService: PhotosService,
              private renderer: Renderer,
              ngZone:NgZone) 
  {
    this.images = photosService.getImages();
    this.baseUrl = photosService.getBaseUrl();
    this.maxPercent = 100;
    this.minPercent = this.maxPercent / this.images.length;
    ngZone.run(() => {
        this.windowWidth = window.innerWidth;
        this.windowHeight = window.innerHeight;
    });
    
    window.onresize = (e) =>
    {
        ngZone.run(() => {
            this.windowWidth = window.innerWidth;
            this.windowHeight = window.innerHeight;
        });
    };
  }

  ngOnInit() {
    this.switchImage(0);
  }

  /*
  @HostListener('touchstart', ['$event'])
  @HostListener('mousedown', ['$event'])
  onStart(event) 
  {
    if (event.touches) 
    {                      // only for touch
      this.removePreviousTouchListeners();    // avoid mem leaks      
      this.touchmoveListenFunc = this.renderer.listen(event.target, 'touchmove', (e) => { this.onMove(e); });
      this.touchendListenFunc = this.renderer.listen(event.target, 'touchend', (e) => { this.removePreviousTouchListeners(); this.onEnd(e); });
      this.touchcancelListenFunc = this.renderer.listen(event.target, 'touchcancel', (e) => { this.removePreviousTouchListeners(); this.onEnd(e); });
    }
  }

  @HostListener('mousemove', ['$event'])
  @HostListener('touchmove', ['$event'])
  onMove(event: MouseEvent) 
  {
    this.switchImage(event.pageX);
    // or clientX
  }
*/

  @HostListener('mousemove', ['$event'])
  onMouseMove(event: MouseEvent) 
  {
    this.switchImage(event.pageX);
    // or clientX
  }

  @HostListener('touchmove', ['$event'])
  onTouchMove(event: TouchEvent) 
  {
    event.preventDefault();
    this.switchImage(event.touches[0].pageX);
    // or clientX
  }

  /*
  @HostListener('mouseup', ['$event'])
  // @HostListener('touchend', ['$event'])     // don't use these as they are added dynamically
  // @HostListener('touchcancel', ['$event']) // don't use these as they are added dynamically
  onEnd(event) 
  {
    // do stuff
  }
*/

  ngOnDestroy() {
    //this.removePreviousTouchListeners();
  }

  /*
  removePreviousTouchListeners() 
  {
    if (this.touchmoveListenFunc !== null)
      this.touchmoveListenFunc();             // remove previous listener
    if (this.touchendListenFunc !== null)
      this.touchendListenFunc();              // remove previous listener
    if (this.touchcancelListenFunc !== null)
      this.touchcancelListenFunc();           // remove previous listener

    this.touchmoveListenFunc = null;
    this.touchendListenFunc = null;
    this.touchcancelListenFunc = null;
  }
*/

  switchImage(pageX)
  {
      var pos = Math.round((pageX / this.windowWidth) * this.maxPercent);
      
      var whichIndex = Math.floor(pos / this.minPercent);
      if (whichIndex > this.images.length - 1)
          whichIndex = this.images.length - 1;

      this.currentImageRelative = `./assets/${this.images[whichIndex]}`;
      this.currentImageAbsolute = `${this.baseUrl}assets/${this.images[whichIndex]}`;
      this.backgroundThing.nativeElement.style.backgroundImage = `url('${this.currentImageAbsolute}')`;

      this.locationString = `${pos}`;//"Penstock Lagoon";
      this.dateString = "05 Aug 2017";
      this.timeString = this.currentImageRelative.replace('src/assets/image', '').replace('.jpg', '').split('_')[1].replace('-', ':').substring(0, 5);
  }
    

}
