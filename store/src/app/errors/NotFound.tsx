import { Button, Divider, Paper, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { Link } from "react-router-dom";

export default function NoFound()
{
    return(
        <Container component={Paper} sx={{height:200}}>
            <Typography gutterBottom variant='h3'>Oops -we could not find what you are looking for</Typography>
            <Divider/>
            <Button fullWidth component={Link} to='/catalog'>Go back to shop</Button>
        </Container>
    )
}