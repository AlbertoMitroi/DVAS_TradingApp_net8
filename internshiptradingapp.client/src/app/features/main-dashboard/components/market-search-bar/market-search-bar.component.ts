import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-market-search-bar',
  templateUrl: './market-search-bar.component.html',
  styleUrls: ['./market-search-bar.component.css'],
})
export class MarketSearchBarComponent {
  public value: string | null = null;
  public suggestions: any[] = [];

  @Output() search = new EventEmitter<any>();

  private searchSubject = new Subject<string>();

  constructor(private http: HttpClient) {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(query => {
      this.fetchSuggestions(query);
    });
  }

  onInput() {
    if (this.value) {
      this.searchSubject.next(this.value.trim());
    } else {
      this.suggestions = [];
    }
  }

  fetchSuggestions(query: string) {
    if (query) {
      this.http
        .get<any[]>(`https://localhost:7221/api/CompanyInventory`)
        .subscribe(
          (response: any[]) => {
            this.suggestions = response.filter(suggestion =>
              suggestion.company.name.toLowerCase().startsWith(query.toLowerCase())
            );
          },
          (error) => {
            console.error('Error fetching suggestions:', error);
          }
        );
    }
  }


  onSuggestionSelect(suggestion: any) {
    this.value = suggestion.company.name;
    this.suggestions = [];
    this.onSearch();
  }

  onSearch() {
    if (this.value && this.value.trim()) {
      this.http
        .get(`https://localhost:7221/api/CompanyInventory?value=${this.value.trim()}`)
        .subscribe(
          (response: any) => {
            this.search.emit(response);
          },
          (error) => {
            console.error('Error fetching company data:', error);
          }
        );
    } else {
      console.error('Please enter a valid symbol.');
    }
  }
}
