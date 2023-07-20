import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';



export default function UploadButton() {
    const navigate = useNavigate();
  return (
    <Button 
      variant="contained"
      onClick={() => navigate('/transactions/upload')} 
    >
      Enviar Arquivo
    </Button>
  );
}