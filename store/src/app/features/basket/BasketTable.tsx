import { Remove, Add, Delete } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Box } from "@mui/material";
import { useState } from "react";
import agent from "../../api/agent";
import { useStoreContext } from "../../context/StoreContext";
import { BasketItem } from "../../models/basket";

interface Props {
    items: BasketItem[];
    isBasket?: boolean;
}

export default function BasketTable({ items, isBasket = true }: Props) {
    
    const { setBasket, removeItem } = useStoreContext();
    const [status, setStatus] = useState({
        loading: false,
        name: ''
    });

    function handleAddItem(productId: number, name: string) {
        setStatus({ loading: true, name: name });
        agent.Basket.addItem(productId)
            .then(basket => setBasket(basket))
            .catch(error => console.log(error))
            .finally(() => setStatus({ loading: false, name: '' }))
    }

    function handleRemoveItem(productId: number, quantity = 1, name: string) {
        setStatus({ loading: true, name: name });
        agent.Basket.removeItem(productId, quantity)
            .then(() => removeItem(productId, quantity))
            .catch(error => console.log(error))
            .finally(() => setStatus({ loading: true, name: '' }))
    }
    return(
    <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }}  >
            <TableHead>
                <TableRow>
                    <TableCell>Products</TableCell>
                    <TableCell align="center">Price</TableCell>
                    <TableCell align="center">Quantity</TableCell>
                    <TableCell align="right">Subtotal</TableCell>
                    {isBasket &&
                        <TableCell align="right"></TableCell>}
                </TableRow>
            </TableHead>
            <TableBody>
                {items.map((item) => (
                    <TableRow
                        key={item.productId}
                        sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                    >
                        <TableCell component="th" scope="row">
                            <Box display='flex' alignItems='center'>
                                <img src={item.pictureUrl} alt={item.name} style={{ height: 50, marginRight: 20 }} />
                                <span>{item.name}</span>
                            </Box>
                        </TableCell>
                        <TableCell align="center">${(item.price / 100).toFixed(2)}</TableCell>
                        <TableCell align="center">
                            {isBasket &&
                                <LoadingButton loading={status.loading && status.name === 'add' + item.productId}
                                    onClick={() => handleRemoveItem(item.productId, 1, 'rem' + item.productId)}
                                    color='secondary'>
                                    <Remove />
                                </LoadingButton>}
                            {item.quantity}
                            {isBasket &&
                                <LoadingButton loading={status.loading && status.name === 'add' + item.productId}
                                    onClick={() => handleAddItem(item.productId, 'add' + item.productId)}
                                    color='secondary'>
                                    <Add />
                                </LoadingButton>}
                        </TableCell>

                        <TableCell align="right">${((item.price / 100) * item.quantity).toFixed(2)}</TableCell>
                        {isBasket &&
                            <TableCell align="right">
                                <LoadingButton loading={status.loading && status.name === 'del' + item.productId}
                                    onClick={() => handleRemoveItem(item.productId, item.quantity, 'del' + item.productId)}
                                    color='error'>
                                    <Delete />
                                </LoadingButton>
                            </TableCell>}
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    </TableContainer>
    )
}