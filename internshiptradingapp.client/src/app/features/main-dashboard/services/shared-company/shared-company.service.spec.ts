import { TestBed } from '@angular/core/testing';

import { SharedCompanyService } from './shared-company.service';

describe('SharedCompanyService', () => {
  let service: SharedCompanyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedCompanyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
