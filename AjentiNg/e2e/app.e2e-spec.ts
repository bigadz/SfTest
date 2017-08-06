import { AjentiNgPage } from './app.po';

describe('ajenti-ng App', () => {
  let page: AjentiNgPage;

  beforeEach(() => {
    page = new AjentiNgPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
