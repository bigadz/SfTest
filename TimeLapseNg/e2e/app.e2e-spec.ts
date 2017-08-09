import { TimeLapseNgPage } from './app.po';

describe('time-lapse-ng App', () => {
  let page: TimeLapseNgPage;

  beforeEach(() => {
    page = new TimeLapseNgPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
