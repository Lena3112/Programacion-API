import { Controller, Get, Post, Param, Put, Delete, Body } from '@nestjs/common';
import { UsersService } from './users.service';
import { User } from './user.entity';

@Controller('users')
export class UsersController {
    constructor(private readonly usersService: UsersService) {}
    
    @Post()
    create(@Body() user: User) {
        return this.usersService.create(user);
    }
    
    @Get()
    findAll() {
        return this.usersService.findAll();
    }
    
    @Get(':id')
    findOne(@Param('id') id: number) {
        return this.usersService.findOne(+id); // el "+id" sirve para convertir el string a number
    }
    
    @Put(':id')
    update(@Param('id') id: number, @Body() user: Partial<User>) {
        return this.usersService.update(id, user);
    }
    
    @Delete(':id')
    remove(@Param('id') id: number) {
        return this.usersService.remove(id);
    }
}
