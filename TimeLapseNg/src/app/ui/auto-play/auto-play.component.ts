import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-auto-play',
  templateUrl: './auto-play.component.html',
  styleUrls: ['./auto-play.component.css']
})
export class AutoPlayComponent implements OnInit {
  @Output() autoplayChanged: EventEmitter<boolean> = new EventEmitter<boolean>();
  state: boolean = false;
  stateText: string = this.getTextForState();
  stateIconUri: string = this.getIconUriForState();


  constructor() { }

  ngOnInit() {
  }

  toggleState($event): void
  {
    this.state = !this.state;
    this.stateText = this.getTextForState();
    this.stateIconUri = this.getIconUriForState();

    if (this.autoplayChanged)
    {
      this.autoplayChanged.emit(this.state);
    }
  }

  private getTextForState(): string
  {
    return this.state ? "paused" : "play";
  }

  private getIconUriForState(): string
  {
    return this.state ? "./assets/16-play.png" : "./assets/17-pause.png";
  }

}
