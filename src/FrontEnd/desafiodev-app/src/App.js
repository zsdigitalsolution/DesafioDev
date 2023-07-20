import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider } from '@mui/material/styles';
import { AppBar, Toolbar } from '@mui/material';
import CssBaseline from '@mui/material/CssBaseline';

import { theme } from './theme';

import TransactionList from './components/TransactionList';
import TransactionForm from './components/TransactionForm'; 
import UploadButton from './components/UploadButton'; 
import UploadFile from './components/UploadFile';
function App() {

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />      
        <BrowserRouter>
        <Routes>
          <Route path="/" element={<TransactionList />} />  
          <Route path="/transactions/new" element={<TransactionForm />} />
          <Route path="/transactions/upload" element={<UploadFile />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;