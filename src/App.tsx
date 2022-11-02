import React, { useEffect, useState } from 'react';
import './App.css';
import { isSbUp } from './services/sbstatus.service'

function App() {
  const [result, setResult] = useState<boolean>(false);

  useEffect(() => {
    const api = async () => {
      const result = await isSbUp();
      setResult(result);
    }

    api();
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <p>
          The main Sponsorblock server is {result ? 'up' : 'down'}
        </p>
      </header>
    </div>
  );
}

export default App;
