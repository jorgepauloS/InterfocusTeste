import { Routes, Route } from 'react-router-dom';
import Home from './Home';
import Clientes from './Clientes';
import DividasCliente from './DividasCliente';

function App() {
  return (
    <Routes>
      <Route path='/' element={ <Home /> } />
      <Route path='/clientes' element={ <Clientes /> } />
      <Route path='/dividas/:id' element={ <DividasCliente /> } />
    </Routes>
  );
}

export default App;
