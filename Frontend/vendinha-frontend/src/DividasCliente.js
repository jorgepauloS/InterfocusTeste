import './DividasClientes.css';
import ApiClient from './Services/ApiClient';
import { useState, useEffect } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import { styled } from '@mui/material/styles';
import { tableCellClasses } from '@mui/material/TableCell';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Stack, Button, Typography, Modal, Box } from '@mui/material';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: theme.palette.common.white,
        color: '#282c34',
    },
    [`&.${tableCellClasses.body}`]: {
        fontSize: 14,
    },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
    '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.action.hover,
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    },
}));

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    pt: 2,
    px: 4,
    pb: 3,
};

function DividasCliente() {
    const navigate = useNavigate();

    //Variáveis
    const [dividas, setDividas] = useState([]);
    const [somaAberto, setSomaAberto] = useState(Number);
    const [somaTotal, setSomaTotal] = useState(Number);
    const [idDivida, setIdDivida] = useState(Number);
    const [pagarDivida, setPagarDivida] = useState(Boolean);

    //Use parameters
    let { id } = useParams();

    const handleOpenPagamentoDividas = (e) => {
        e.preventDefault();
        setIdDivida(e.target.ariaLabel);
        setPagarDivida(true);
    }
    
    const handleClosePagamentoDividas = (e) => {
        e.preventDefault();
        setIdDivida(0);
        setPagarDivida(false);
    }

    const submitCadastroDividas = (e) => {
        e.preventDefault();
        ApiClient().PagarDivida(idDivida)
            .then((response) => {
                alert('Dívida marcada como paga.');
                handleClosePagamentoDividas(e);
                navigate('/clientes');
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    }

    useEffect(() => {
        ApiClient().GetDividasPorCliente(id)
            .then((response) => {
                setDividas(response.data.data.dividas);
                setSomaAberto(response.data.data.somaAberto);
                setSomaTotal(response.data.data.somaTotal);
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    }, [id]);

    return (
        <div className='Dividas'>
            <TableContainer component={Paper} sx={{ width: '95vw', height: '75vh' }}>
                <Table>
                    <TableHead>
                        <TableRow sx={{ bgColor: 'white' }}>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell align="left">Valor</StyledTableCell>
                            <StyledTableCell align="left">Situação</StyledTableCell>
                            <StyledTableCell align="left">Data de pagamento</StyledTableCell>
                            <StyledTableCell align="left">Ações</StyledTableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {dividas.length > 0 && dividas.map((row) => (
                            <StyledTableRow key={row.id}>
                                <StyledTableCell component="th" scope="row">
                                    {row.id}
                                </StyledTableCell>
                                <StyledTableCell align="left">{row.valor}</StyledTableCell>
                                <StyledTableCell align="left">{row.situacao === 0 ? 'Em Aberto' : 'Paga'}</StyledTableCell>
                                <StyledTableCell align="left">{row.dataPagamento}</StyledTableCell>
                                <StyledTableCell align="left">
                                    {
                                        row.situacao === 0 &&
                                        <Stack spacing={1} direction="column">
                                            <Button aria-label={row.id} variant="contained" onClick={handleOpenPagamentoDividas}>
                                                Pagar dívida
                                            </Button>
                                        </Stack>
                                    }
                                </StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Typography>
                Soma das dívidas em aberto: {somaAberto}
            </Typography>
            <Typography>
                Soma de todas as dívidas: {somaTotal}
            </Typography>
            <Modal onClose={handleClosePagamentoDividas} open={pagarDivida}>
                <Box sx={{ ...style }}>
                    <Stack spacing={2} direction="column">
                        <h2>Quitar dívida</h2>
                        <Stack spacing={1} direction="row">
                            <Button onClick={handleClosePagamentoDividas}>Cancelar</Button>
                            <Button onClick={submitCadastroDividas}>Marcar como paga</Button>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
        </div>
    );
}

export default DividasCliente;