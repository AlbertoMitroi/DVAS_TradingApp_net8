import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TopXTableComponent } from './top-xtable.component';
import { SharedCompanyService } from '../../services/shared-company/shared-company.service';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';

describe('TopXTableComponent', () => {
  let component: TopXTableComponent;
  let fixture: ComponentFixture<TopXTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TopXTableComponent],
      imports: [
        HttpClientTestingModule,
        MatCardModule,
        MatTableModule 
      ],
      providers: [SharedCompanyService]
    }).compileComponents();
    
    fixture = TestBed.createComponent(TopXTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
