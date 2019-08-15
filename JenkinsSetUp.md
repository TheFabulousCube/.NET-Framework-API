# Jenkins Set Up for TaDaa Tie Dye 


## Branching on GitHub
***
There are many reasons I wanted to use branching.
* I can develop on the `dev` branch freely and reset my code without disturbing production ready code.
* I can deploy to `qa` and test locally using IIS instead of IID Express (**not the same thing!**)
* The `dev` and `qa` branches point to a local copy of the database.  I can freely test and not worry about messing up the `prod`uction database.
* I can set up feature branches and test incrementally until they are ready or drop the branch completely.  This creates a _seperation of concern_ between developing code and releasing features.
* I wanted to learn more about it.  Experience is the best teacher for me.

> In order to simplify managing the branches, I decided to use **Jenkins** to manage continuous integration and delivery.  **Jenkins** can automate many of the steps needed to manage builds, testing, and deployment.  That means I can focus on writing the code instead of managing it.  (This also looked **fun**!)

### I decided on 4 environments (although I've got ideas for dynamically created branches!)
1) `dev` - This is my main working branch.  I **only** push to the development branch.
2) `qa`  - This is the main testing branch.  It's deployed to my locall IIS, so it's similar enough to production to be valid, but quick enough (and isolated from disaster!) to do extensive testing.
3) `staging` - This should be the last step before releasing to production.  Staging requires a manual deploy after testing in the `qa` environment looks good.  Staging is deployed to the same shared hosting as production, this is where differences between my local IIS and the shared hosting IIS show up. 
4) `production` - This is where it all happens, it's live.

## Jenkins Jobs
***
### Build to QA
This job is triggered by a push to the `dev` branch.  When I'm satisfied with a body of work, or I just hit a good stopping place, I can push some code and let Jenkins handle the rest.  
1) Pull code from the gitHub `dev` branch
2) Build the solution
3) Run Unit tests
4) If all the previous steps complete and pass, Jenkins will merge the branch with `qa`
5) Call **Deploy to QA**

### Deploy to QA
I purposly seperated these jobs and chained them together instead of combining them into 1 job to provide more flexibilty.  It could have also been triggered by a push into `qa`.
This job *can* be manually triggered, but it normally will be called by a sucessful compltion of **Build to QA**
1) The first step rebuilds the merged code, this time creating a batch file for deployment
2) (I _can_ run the Unit tests again, but I don't)
2) Runs the auto-generated batch file for deployment

### Deploy to Staging
This is still a manually triggered job.  If I set up integration testing, I could trigger this job on the sucessful completion of that testing.
Otherwise it's similar to **Deploy to QA**
1) Pull the code from `qa` branch
2) Build the solution, generating the batch file for deployment
3) Run Unit tests (again)
4) Replace the auto-generated `SetParameters.xml` file with one from my local C:\ drive.
> `SetParameters.xml` contains the connection string to the database.  By default, it will include the connection string to my local copy.  I need to change that to the connection string for the copy hosted on the server, but I don't want to publish that connection string to GitHub.  I'm using a copy from my C:\ because it's a secure environment.  This also lets me re-use the same `SetParameters.xml` for any other project that needs to be built the same wqy.
5) Runs the auto-generated batch file using the adjusted `SetParameters.xml` file to deploy to the shared hosting server

### Deploy to Prod
This job is the same as **Deploy to Staging** except it will pull from `staging` and deploy to the shared server at api.tadaatiedye.com

## Update!
The **Build To QA** job is on build #47, several fail, a couple pass.   Then I made another change and several more fail, then a few pass.  The **Deploy To QA** is on about build #24 some pass some fail.  By the time I got to **Deploy To Staging**, I had a lot of issues deploying to the server, back up to #55, with *lots* of failures.  But all that hard work and patience must have paid off, **Deploy To Prod** is on #7 without a single failure (yet)!  
I noticed a spelling error in the Swagger on Production.  I had to go back to Visual Studio to make the change & build the change up the chain, it took about 10 minutes from defect to fixed!  That's why I did this!
## Issues & Problems (`Here, there be Dragons!`)
* None of the packages would install on the QA build.  I really racked my brain on this one.  I checked the location of the files and they are there.  I couldn't figure out why they build couldn't pick them up from the default location, so I copied one into a folder defined in a 'Hint Path'.  That worked, but it's not right.  *Finally* I noticed that the path is a relative path.  Sure enough, .gitignore excluded the packages folder for me.  (I guess it's to save space., they _can_ be downloaded again.)  I figured I had three options:
  1) Remove the exclusion from .gitignore.  This meant copying a pretty large amount of redundant data all over the place.  I don't like this.
  2) Copy all of the project-specific packages to the central repository for packages.  This could make sense in the long run, many will be reused.
  3) Change all the paths to absolute paths.  Since I'm only running the build on my local, the path will be correct.    
  
  #3 was the least disruptive and took the least amount of space.  If you try to build the project on _your_ computer, make sure to install the packages.

* I downloaded a `.publishsettings` file from my host and included it in my project to use for deployment.  My local build just fine, but the QA build kept failing.  I followed the defined path, and the file **is** there.  Then I realized it's a *relative* path.  Checking the Jenkins workspace, I realized that the file **really is** missing. *Finally* I realized that the `.gitignore` file excluded `.publishsettings` files, so that's why it's not included in the workspace.  I fixed the `.gitignore` file and Git Extensions picked it up on the next push.