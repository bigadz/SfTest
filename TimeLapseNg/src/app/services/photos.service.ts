import { Injectable } from '@angular/core';

@Injectable()
export class PhotosService {

  private images = ['image17-08-05_06-59-58-35.jpg','image17-08-05_07-04-58-36.jpg','image17-08-05_07-09-58-37.jpg','image17-08-05_07-14-58-38.jpg','image17-08-05_07-19-58-38.jpg','image17-08-05_07-24-58-39.jpg','image17-08-05_07-29-58-35.jpg','image17-08-05_07-34-58-36.jpg','image17-08-05_07-39-58-36.jpg','image17-08-05_07-44-58-37.jpg','image17-08-05_07-49-58-38.jpg','image17-08-05_07-54-58-38.jpg','image17-08-05_07-59-58-39.jpg','image17-08-05_08-04-58-40.jpg','image17-08-05_08-09-58-40.jpg','image17-08-05_08-14-58-41.jpg','image17-08-05_08-19-58-41.jpg','image17-08-05_08-24-58-42.jpg','image17-08-05_08-29-58-42.jpg','image17-08-05_08-34-58-43.jpg','image17-08-05_08-39-58-39.jpg','image17-08-05_08-44-58-41.jpg','image17-08-05_08-49-58-41.jpg','image17-08-05_08-54-58-42.jpg','image17-08-05_08-59-58-41.jpg','image17-08-05_09-04-58-42.jpg','image17-08-05_09-09-58-42.jpg','image17-08-05_09-14-58-42.jpg','image17-08-05_09-19-58-42.jpg','image17-08-05_09-24-58-43.jpg','image17-08-05_09-29-58-43.jpg','image17-08-05_09-34-58-40.jpg','image17-08-05_09-39-58-40.jpg','image17-08-05_09-44-58-38.jpg','image17-08-05_09-49-58-39.jpg','image17-08-05_09-54-58-39.jpg','image17-08-05_09-59-58-40.jpg','image17-08-05_10-04-58-40.jpg','image17-08-05_10-09-58-41.jpg','image17-08-05_10-14-58-41.jpg','image17-08-05_10-19-58-41.jpg','image17-08-05_10-24-58-42.jpg','image17-08-05_10-29-58-42.jpg','image17-08-05_10-34-58-43.jpg','image17-08-05_10-39-58-43.jpg','image17-08-05_10-44-58-44.jpg','image17-08-05_10-49-58-44.jpg','image17-08-05_10-54-58-42.jpg'];

  constructor() { }

  getImages(): Array<string>
  {
    return this.images;
  }
  
  getBaseUrl(): string 
  {
    return location.protocol + '//' + location.hostname + (location.port ? ':'+location.port : '') + '/'
  }
}
