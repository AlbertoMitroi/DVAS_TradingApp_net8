import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopXTableComponent } from './top-xtable.component';

describe('TopXTableComponent', () => {
  let component: TopXTableComponent;
  let fixture: ComponentFixture<TopXTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TopXTableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TopXTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
