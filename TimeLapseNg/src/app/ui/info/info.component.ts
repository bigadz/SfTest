import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {
  @Input() locationString: string;
  @Input() dateString: string;
  @Input() timeString: string;
 
  constructor() { }

  ngOnInit() {
  }

}
