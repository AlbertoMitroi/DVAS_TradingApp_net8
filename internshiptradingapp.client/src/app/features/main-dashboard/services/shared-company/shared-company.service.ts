import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedCompanyService {
  constructor() {}
  private selectedCompanySubject = new BehaviorSubject<any>(null);

  selectedCompany$ = this.selectedCompanySubject.asObservable();

  setValue(value: any) {
    this.selectedCompanySubject.next(value);
  }
}
