import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders status', () => {
  render(<App />);
  const linkElement = screen.getByText(/The main Sponsorblock server is/i);
  expect(linkElement).toBeInTheDocument();
});
