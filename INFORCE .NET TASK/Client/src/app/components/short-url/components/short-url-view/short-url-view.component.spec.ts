import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortUrlViewComponent } from './short-url-view.component';

describe('ShortUrlViewComponent', () => {
  let component: ShortUrlViewComponent;
  let fixture: ComponentFixture<ShortUrlViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShortUrlViewComponent]
    });
    fixture = TestBed.createComponent(ShortUrlViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
