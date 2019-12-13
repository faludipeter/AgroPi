AgroPi
=======

## verison: version_0.1

### Requirements:
- MySQL 5.5 or higher
### Bugfix:



### TODO!:


------
### Git config
```javascript
git config --global user.name "Faludi Péter"
git config --global user.email "peter.faludi@dilaco.hu"
```
#### Create a new repository
```javascript
git clone git@git.dilaco.hu:peter.faludi/dilaco-agropi.git
cd dilaco-agropi
touch README.md
git add README.md
git commit -m "add README"
git push -u origin master
```

#### Existing folder
```javascript
cd existing_folder
git init
git remote add origin git@git.dilaco.hu:peter.faludi/dilaco-agropi.git
git add .
git commit -m "Initial commit"
git push -u origin master
```

#### Existing Git repository
```javascript
cd existing_repo
git remote rename origin old-origin
git remote add origin git@git.dilaco.hu:peter.faludi/dilaco-agropi.git
git push -u origin --all
git push -u origin --tags
```

### 2019.11.28