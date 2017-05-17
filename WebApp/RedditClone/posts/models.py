from django.db import models
from django.contrib.auth.models import User


class Post(models.Model):
    title = models.CharField(max_length=200)
    url = models.TextField()
    datePublished = models.DateTimeField()
    author = models.ForeignKey(User)
    votesTotal = models.IntegerField(default=1)

    def pubDateCleanUp(self):
        return self.datePublished.strftime('%b %e %Y')