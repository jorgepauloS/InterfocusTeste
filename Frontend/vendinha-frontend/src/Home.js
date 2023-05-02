import './Home.css';
import { Button, Stack } from "@mui/material"

function Home() {
    return (
        <div className="Home">
            <header className="Home-header">
                <Stack spacing={2} direction="column">
                    <Button variant="contained" href='clientes'>Clientes</Button>
                    <Button variant="contained" href='dividas'>Dívidas</Button>
                </Stack>
            </header>
        </div>
    )
}

export default Home;