import './Clientes.css';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import ApiClient from './Services/ApiClient';
import { styled } from '@mui/material/styles';
import { tableCellClasses } from '@mui/material/TableCell';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TablePagination, TableRow, Stack, Button, Typography, Modal, Box, TextField } from '@mui/material';

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

function Clientes() {
    const navigate = useNavigate();

    //Variáveis
    const [clientesRetorno, setClientesRetorno] = useState([]);
    const [page, setPage] = useState(Number);
    const [total, setTotal] = useState(Number);
    const [filteredName, setFilteredName] = useState(String);
    const [soma, setSoma] = useState(Number);
    const [cadastrarDivida, setCadastrarDivida] = useState(Boolean);
    const [idCliente, setIdCliente] = useState(Number);
    const [valorCadastroDivida, setValorCadastroDivida] = useState(Number);
    const [apagarCliente, setApagarCliente] = useState(Boolean);
    const [cadastrarCliente, setCadastrarCliente] = useState(Boolean);
    const [alterarCliente, setAlterarCliente] = useState(Boolean);
    const [nomeCliente, setNomeCliente] = useState(Number);
    const [cpfCliente, setCpfCliente] = useState(String);
    const [emailCliente, setEmailCliente] = useState(String);
    const [dataNascimentoCliente, setDataNascimentoCliente] = useState(Date);

    const handleOpenCadastrarCliente = (e) => {
        e.preventDefault();
        setNomeCliente('');
        setCpfCliente('');
        setEmailCliente('');
        setDataNascimentoCliente('');
        setCadastrarCliente(true);
    };

    const handleCloseCadastrarCliente = (e) => {
        e.preventDefault();
        setNomeCliente('');
        setCpfCliente('');
        setEmailCliente('');
        setDataNascimentoCliente('');
        setCadastrarCliente(false);
    };

    const submitCadastrarCliente = (e) => {
        e.preventDefault();
        ApiClient().CadastrarCliente(nomeCliente, cpfCliente, dataNascimentoCliente, emailCliente)
            .then((response) => {
                alert('Cliente cadastrado com sucesso.');
                handleCloseCadastrarCliente(e);
                navigate('/');
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    };

    const handleOpenAlterarCliente = (e) => {
        e.preventDefault();
        setIdCliente(e.target.ariaLabel);
        for (let i = 0; i < clientesRetorno.length; i++) {
            if (String(clientesRetorno[i].id) === String(e.target.ariaLabel)) {
                setNomeCliente(clientesRetorno[i].nome);
                setCpfCliente(clientesRetorno[i].cpf);
                setEmailCliente(clientesRetorno[i].email);
                setDataNascimentoCliente(clientesRetorno[i].dataNascimento);
            }
        }
        setAlterarCliente(true);
    };

    const handleCloseAlterarCliente = (e) => {
        e.preventDefault();
        setIdCliente(0);
        setNomeCliente('');
        setCpfCliente('');
        setEmailCliente('');
        setDataNascimentoCliente('');
        setAlterarCliente(false);
    };

    const submitAlterarCliente = (e) => {
        e.preventDefault();
        ApiClient().AlterarCliente(idCliente, nomeCliente, cpfCliente, dataNascimentoCliente, emailCliente)
            .then((response) => {
                alert('Cliente alterado com sucesso.');
                handleCloseCadastrarCliente(e);
                navigate('/');
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    };

    const handleChangePage = (e, newPage) => {
        if (page !== newPage) {
            setPage(newPage);
        }
    };

    const handleOpenCadastroDividas = (e) => {
        e.preventDefault();
        setIdCliente(e.target.ariaLabel);
        setCadastrarDivida(true);
    };

    const handleCloseCadastroDividas = (e) => {
        e.preventDefault();
        setIdCliente(0);
        setCadastrarDivida(false);
    };

    const submitCadastroDividas = (e) => {
        e.preventDefault();
        ApiClient().CadastrarDivida(idCliente, valorCadastroDivida)
            .then((response) => {
                alert('Dívida cadastrada com sucesso.');
                handleCloseCadastroDividas(e);
                navigate('/dividas/' + idCliente);
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    };

    const handleOpenApagarCliente = (e) => {
        e.preventDefault();
        setIdCliente(e.target.ariaLabel);
        setApagarCliente(true);
    };

    const handleCloseApagarCliente = (e) => {
        e.preventDefault();
        setIdCliente(0);
        setApagarCliente(false);
    };

    const submitApagarCliente = (e) => {
        e.preventDefault();
        ApiClient().ApagarCliente(idCliente)
            .then((response) => {
                alert('Cliente apagado com sucesso.');
                handleCloseApagarCliente(e);
                navigate('/');
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    };

    useEffect(() => {
        ApiClient().GetClientes(page + 1, filteredName)
            .then((response) => {
                setClientesRetorno(response.data.data);
                setPage(response.data.currentPage - 1);
                setTotal(response.data.totalRecords);

                var dividaTotal = Number(0);

                response.data.data.forEach((cliente) => {
                    dividaTotal += cliente.dividaCliente;
                });

                setSoma(dividaTotal);
            })
            .catch((error) => {
                if (error.response.data.message) {
                    alert(error.response.data.message);
                }
                else {
                    alert('Tente novamente em alguns instantes.');
                }
            });
    }, [page, filteredName, soma]);

    return (
        <div className="Clientes">
            <Stack spacing={1} direction="column">
                <Button onClick={handleOpenCadastrarCliente}>Cadastrar Cliente</Button>
                <TableContainer component={Paper} sx={{ width: '95vw', height: '75vh' }}>
                    <Table>
                        <TableHead>
                            <TableRow sx={{ bgColor: 'white' }}>
                                <StyledTableCell>Id</StyledTableCell>
                                <StyledTableCell align="left">
                                    <Stack spacing={2} direction="column">
                                        Nome
                                        <TextField label="Filtrar" variant="filled" onChange={(e) => setFilteredName(e.target.value)} sx={{ bg: 'white' }} />
                                    </Stack>
                                </StyledTableCell>
                                <StyledTableCell align="right">Idade</StyledTableCell>
                                <StyledTableCell align="right">CPF</StyledTableCell>
                                <StyledTableCell align="right">Dívida</StyledTableCell>
                                <StyledTableCell align="right">Email</StyledTableCell>
                                <StyledTableCell align="right">Ações</StyledTableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {clientesRetorno.length > 0 && clientesRetorno.map((row) => (
                                <StyledTableRow key={row.id}>
                                    <StyledTableCell component="th" scope="row">
                                        {row.id}
                                    </StyledTableCell>
                                    <StyledTableCell align="right">{row.nome}</StyledTableCell>
                                    <StyledTableCell align="right">{row.idade}</StyledTableCell>
                                    <StyledTableCell align="right">{row.cpf}</StyledTableCell>
                                    <StyledTableCell align="right">{row.dividaCliente}</StyledTableCell>
                                    <StyledTableCell align="right">{row.email}</StyledTableCell>
                                    <StyledTableCell align="right">
                                        <Stack spacing={1} direction="column">
                                            <Button variant="contained" href={'dividas/' + String(row.id)}>
                                                Dívidas
                                            </Button>
                                            <Button variant="contained" aria-label={row.id} onClick={handleOpenCadastroDividas}>
                                                Cadastrar dívida
                                            </Button>
                                            <Button variant="contained" aria-label={row.id} onClick={handleOpenAlterarCliente}>
                                                Atualizar cliente
                                            </Button>
                                            <Button variant="contained" aria-label={row.id} onClick={handleOpenApagarCliente}>
                                                Apagar cliente
                                            </Button>
                                        </Stack>
                                    </StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                    {
                        clientesRetorno != null &&
                        <TablePagination
                            rowsPerPageOptions={[10]}
                            component="div"
                            count={total}
                            rowsPerPage={10}
                            page={page}
                            onPageChange={handleChangePage}
                        />
                    }
                </TableContainer>
                <Typography>
                    Soma das dívidas: {soma}
                </Typography>
            </Stack>
            <Modal onClose={handleCloseCadastroDividas} open={cadastrarDivida}>
                <Box sx={{ ...style }}>
                    <Stack spacing={2} direction="column">
                        <h2>Cadastrar dívida</h2>
                        <TextField
                            label="Valor da dívida"
                            variant="filled"
                            type="number"
                            onChange={(e) => setValorCadastroDivida(e.target.value)}
                            sx={{ bg: 'white' }}
                        >
                        </TextField>
                        <Stack spacing={1} direction="row">
                            <Button onClick={handleCloseCadastroDividas}>Cancelar</Button>
                            <Button onClick={submitCadastroDividas}>Cadastrar Dívida</Button>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
            <Modal onClose={handleCloseApagarCliente} open={apagarCliente}>
                <Box sx={{ ...style }}>
                    <Stack spacing={2} direction="column">
                        <h2>Apagar Cliente?</h2>
                        <Stack spacing={1} direction="row">
                            <Button onClick={handleCloseApagarCliente}>Cancelar</Button>
                            <Button onClick={submitApagarCliente}>Confirmar</Button>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
            <Modal onClose={handleCloseCadastrarCliente} open={cadastrarCliente}>
                <Box sx={{ ...style }}>
                    <Stack spacing={2} direction="column">
                        <h2>Cadastrar Cliente</h2>
                        <TextField label="Nome" variant="filled" onChange={(e) => setNomeCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="CPF" variant="filled" onChange={(e) => setCpfCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="Data de Nascimento" type="date" defaultValue={null} variant="filled" onChange={(e) => setDataNascimentoCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="Email" type="email" variant="filled" onChange={(e) => setEmailCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <Stack spacing={1} direction="row">
                            <Button onClick={handleCloseCadastrarCliente}>Cancelar</Button>
                            <Button onClick={submitCadastrarCliente}>Cadastrar</Button>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
            <Modal onClose={handleCloseAlterarCliente} open={alterarCliente}>
                <Box sx={{ ...style }}>
                    <Stack spacing={2} direction="column">
                        <h2>Alterar Cliente</h2>
                        <TextField label="Nome" value={nomeCliente} variant="filled" onChange={(e) => setNomeCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="CPF" value={cpfCliente} variant="filled" onChange={(e) => setCpfCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="Data de Nascimento" value={dataNascimentoCliente} type="date" defaultValue={null} variant="filled" onChange={(e) => setDataNascimentoCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <TextField label="Email" value={emailCliente} type="email" variant="filled" onChange={(e) => setEmailCliente(e.target.value)} sx={{ bg: 'white' }} />
                        <Stack spacing={1} direction="row">
                            <Button onClick={handleCloseCadastrarCliente}>Cancelar</Button>
                            <Button onClick={submitAlterarCliente}>Alterar</Button>
                        </Stack>
                    </Stack>
                </Box>
            </Modal>
        </div >
    );
}

export default Clientes;
